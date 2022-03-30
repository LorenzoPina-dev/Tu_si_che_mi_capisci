var express = require("express");
var router = express.Router();
const fs = require("fs");
var sha256 = require("js-sha256").sha256;
var db = require("./../util/db");
var nodemailer = require("nodemailer");
const file = `./codiciReset/codiciReset${formatDate(new Date())}.txt`;
let emailRegexp = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
var transporter = nodemailer.createTransport({
    service: "Gmail",
    auth: {
        user: process.env.MAILUSER,
        pass: process.env.MAILPASS,
    },
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
    console.log(query)

    if (!query.username || !query.password) {
        res.json({ success: false, result: { testo: "mancano parametri o sono errati" } });
        return;
    }
    console.log(sha256.hex(query.password));
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
            console.log(result);
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
    console.log(query);
    if (!query.username || !query.password || !query.mail || !query.password2 || !emailRegexp.test(query.mail)) {
        res.json({ success: false, result: { testo: "mancano parametri o sono errati" } });
        return;
    }
    console.log("dopo")
    if (query.password != query.password2) {
        res.json({ success: false, result: { testo: "password non corrispondono" } });
        console.log("pass");
        return;
    }
    console.log("inizio");
    let d = new Date().getTime();
    key = "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function(c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == "x" ? r : (r & 0x3) | 0x8).toString(16);
    });
    console.log("primaCript");
    let criptata;
    criptata = sha256.hex(query.password);
    console.log(criptata);
    db.query(
        "INSERT INTO utente (Username, Password, Email, ApiKey) VALUES (?,?,?,?)", [query.username, criptata, query.mail, key],
        (err, result) => {
            if (err) {
                res.json({ success: false, result: { testo: "errore nell'inserimento" } });
                throw err
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
    let codici = [];
    if (fs.existsSync(file))
        codici = fs.readFileSync(file, "utf8").split("\r\n");
    do {
        for (let i = 0; i < 7; i++) codice += getRndInteger(0, 10).toString();
    } while (codici.includes(JSON.stringify({ mail: mail, codice: codice })));
    if (!fs.existsSync(file))
        fs.appendFileSync(file, JSON.stringify({ mail: mail, codice: codice }));
    else
        fs.appendFileSync(file, "\r\n" + JSON.stringify({ mail: mail, codice: codice }));

    console.log(mail)
        //sendMail(res, mail, mail, "codice reset password", "il codice per il reset è: " + codice);
    res.json({ success: true, result: { testo: "invio codice" } });
});
router.put("/cambiaPassword", (req, res) => {
    let query = req.body;
    console.log(query)
    if (!query.password || !query.password2) {
        res.json({
            success: false,
            result: { testo: "mancano parametri o sono errati" },
        });
        return;
    }
    let codici = fs.readFileSync(file, "utf8").split("\r\n").map(val => JSON.parse(val));
    let mail = "";
    for (c of codici)
        if (c.codice == query.codice)
            mail = c.mail;
    if (query.password != query.password2) {
        res.json({ success: false, result: { testo: "le password non coincidono" } });
        return;
    }
    db.query(
        "UPDATE utente SET Password=? where Email=?", [sha256.hex(query.password), mail],
        (err, result) => {
            if (err) {
                res.json({
                    success: false,
                    result: { testo: "Errore" }
                });
                return;
            }
            res.json({
                success: true,
                result: { testo: "update avvenuto con successo" }
            });
        }
    );
});

function getRndInteger(min, max) {
    return Math.floor(Math.random() * (max - min)) + min;
}
const sendMail = (res, username, mailTo, subject, body) => {
    var mailOptions = {
        from: "'Pina Lorenzo' MailPerTest.01@gmail.com",
        to: `${username} ${mailTo}`,
        subject: subject,
        html: body,
    };
    try {
        transporter.sendMail(mailOptions, function(error, info) {
            if (error) {
                console.log(error);
            } else {
                console.log("Messaggio inviato: " + info.response);
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