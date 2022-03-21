var express = require('express');
var router = express.Router();
const fs = require('fs');
var multipart = require("connect-multiparty");
var multipartMiddleware = multipart({ uploadDir: "uploads/" });
var sha256 = require("js-sha256").sha256;
var db = require("./../util/db");
var nodemailer = require("nodemailer");
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
    db.connect();
    console.log(req.query);
    db.query(
        "SELECT * FROM utente WHERE Username = ? AND Password = ?", [req.query.username, req.query.password],
        (err, result) => {
            if (err) throw err;
            console.log(result);
            db.destroy();
            res.json({ success: true, result: { Id: result[0].Id, ApiKey: result[0].ApiKey } });
        }
    );
});

router.post("/register", (req, res) => {
    console.log("inizio")
    let query = req.query;
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
        "INSERT INTO utente (Username, Password, Email, Immagine, ApiKey) VALUES (?,?,?,?,?)", [query.username, criptata, query.email, query.immagine, key],
        (err, result) => {
            if (err) throw err;
            console.log(result);
            res.json({
                success: true,
            });
        }
    );
});
router.get("/resetPassword", (req, res) => {
    let codice = "";
    let codici = fs.readFileSync(`codiciReset${formatDate(new Date())}.txt`, "utf8").split("\r\n");
    do {
        for (let i = 0; i < 7; i++) codice += getRndInteger(0, 10).toString();
    } while (codici.includes(codice))
    codici.push(codice);
    fs.appendFileSync(`codiciReset${formatDate(new Date())}.txt`, "\r\n" + codice);
    //sendMail(res,req.query.email,req.query.email,"codice reset password","il codice per il reset è: " + codice);
    res.json({ success: true, message: "invio codice" });
})
router.put("/cambiaPassword/:codice", (req, res) => {
    db.query(
        "INSERT INTO utente (Username, Password, Email, Immagine, ApiKey) VALUES (?,?,?,?,?)", [query.username, criptata, query.email, query.immagine, key],
        (err, result) => {
            if (err) throw err;
            console.log(result);
            res.json({
                success: true,
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
    } catch (e) { console.log(e) }
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