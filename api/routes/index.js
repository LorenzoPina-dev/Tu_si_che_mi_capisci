var express = require("express");
var router = express.Router();
const fs = require("fs");
var sha256 = require("js-sha256").sha256;
var db = require("./../util/db");
var nodemailer = require("nodemailer");
let emailRegexp = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
var transporter = nodemailer.createTransport({
    host: "smtp.gmail.com",
    secureConnection: true,
    port: 587,
    requiresAuth: true,
    auth: {
        user: process.env.MAILUSER,
        pass: process.env.MAILPASS,
    }
});
/*router.get('/', function(req, res) {
    res.header('Content-type', 'application/json');
    res.end("{seccess:true}")
})
var i = 0;
router.post("/", multipartMiddleware, function(req, res, next) {
    console.log(req.files.img);
    fs.renameSync(req.files.img.path, 'uploads\\' + (i++) + "." + req.files.img.path.split('.')[1]);
    res.json({ message: "Successfully uploaded files" });
});*/
router.post("/login", (req, res) => {
    query = req.body;
    console.log(query);
    if (!query.username || !query.password) {
        res.json({ success: false, result: { testo: "mancano parametri o sono errati" } });
        return;
    }
    db.query(
        "SELECT * FROM utente WHERE Username = ? AND Password = ?", [query.username, sha256.hex(query.password)],
        (err, result) => {
            if (err) {
                res.json({
                    success: false,
                    result: { testo: "Errore" }
                });
                return;
            }
            if (result.length > 0) {
                res.json({
                    success: true,
                    result: { ApiKey: result[0].ApiKey },
                });
            } else
                res.json({
                    success: false,
                    result: { testo: "username o password errati" },
                });
        }
    );
});

router.post("/register", (req, res) => {
    let query = req.body;
    if (!query.username || !query.password || !query.mail || !query.password2 || !emailRegexp.test(query.mail)) {
        res.json({ success: false, result: { testo: "mancano parametri o sono errati" } });
        return;
    }
    if (query.password != query.password2) {
        res.json({ success: false, result: { testo: "password non corrispondono" } });
        console.log("pass");
        return;
    }

    let d = new Date().getTime();
    key = "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function(c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == "x" ? r : (r & 0x3) | 0x8).toString(16);
    });
    let criptata;
    criptata = sha256.hex(query.password);
    db.query(
        "INSERT INTO utente (Username, Password, Email, ApiKey) VALUES (?,?,?,?)", [query.username, criptata, query.mail, key],
        (err, result) => {
            if (err) {
                res.json({ success: false, result: { testo: "errore nell'inserimento" } });
                return;
            }
            res.json({ success: true, result: { testo: "registrazione avvenuta con successo" } });
        }
    );
});
router.get("/resetPassword", (req, res) => {
    let codice = "";
    let mail = req.query.mail;
    if (!mail || !emailRegexp.test(mail)) {
        res.json({ success: false, result: { testo: "mail errata" } });
        return;
    }
    for (let i = 0; i < 9; i++) codice += getRndInteger(0, 10).toString();

    db.query(
        "Insert into reset(CodiceReset,Mail) values(?,?)", [codice, mail],
        (err, result) => {
            if (err) {

                res.json({
                    success: false,
                    result: { testo: "Errore" },
                });
                return;
            }
            sendMail(res, mail, mail, "codice reset password", "il codice per il reset è: " + codice);
            res.json({ success: true, result: { testo: "invio codice" } });

        }
    );


});
router.put("/cambiaPassword", (req, res) => {
    let query = req.body;
    console.log(query);
    if (!query.password || !query.password2 || !query.codice) {
        res.json({
            success: false,
            result: { testo: "mancano parametri o sono errati" },
        });
        return;
    }
    if (query.password != query.password2) {
        res.json({ success: false, result: { testo: "le password non coincidono" } });
        return;
    }
    db.query(
        "Select Mail from reset WHERE CodiceReset=? and now()-Data<'70000'", [query.codice],
        (err, result) => {
            if (err) {
                console.log(err);
                res.json({
                    success: false,
                    result: { testo: "Errore" },
                });
                return;
            }

            if (result) {
                db.query(
                    "UPDATE utente SET Password=? where Email=?", [sha256.hex(query.password), result],
                    (err, result) => {
                        if (err) {
                            res.json({
                                success: false,
                                result: { testo: "Errore" },
                            });
                            return;
                        }
                        res.json({
                            success: true,
                            result: { testo: "update avvenuto con successo" },
                        });
                    }
                );

            }
        }
    );

});

function getRndInteger(min, max) {
    return Math.floor(Math.random() * (max - min)) + min;
}
const sendMail = (res, username, mailTo, subject, body) => {
    var mailOptions = {
        from: process.env.MAILUSER,
        to: `${username} ${mailTo}`,
        subject: subject,
        text: body
    };
    try {
        transporter.sendMail(mailOptions, function(error, info) {
            if (error) {
                console.log(error);
            }
        });
    } catch (e) {
        console.log(e);
    }
};

function padTo2Digits(num) {
    return num.toString().padStart(2, "0");
}

function formatDate(date) {
    return [
        padTo2Digits(date.getDate()),
        padTo2Digits(date.getMonth() + 1),
        date.getFullYear(),
    ].join("_");
}
module.exports = router;