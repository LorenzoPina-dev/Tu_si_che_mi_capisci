var express = require("express");
var router = express.Router();
const fs = require("fs");
const db = require("../util/db");
var multipart = require("connect-multiparty");
const path = require("path");
let filePath = path.join(
    __dirname,
    "../uploads/voltotrovato"
);
var nodemailer = require("nodemailer");

var transporter = nodemailer.createTransport({
    host: "smtp.gmail.com",
    secureConnection: true,
    port: 587,
    requiresAuth: true,
    auth: {
        user: process.env.MAILUSER,
        pass: process.env.MAILPASS,
    },
});


var multipartMiddleware = multipart({ uploadDir: filePath });
router.get("/", function(req, res) {
    let query = req.query;
    let sql = "SELECT * from voltotrovato where IdUtente=? AND";
    let parametri = [req.Utente.Id];

    if (query.start || (query.data && new Date(query.data).getTime())) {
        if (query.start) {
            sql += " Id>=? AND";
            parametri.push(parseInt(query.start));
        }
        if (query.data) {
            let date = (sql += " DataRilevazione>=? AND");
            parametri.push(query.data);
        }
    }
    sql = sql.substring(0, sql.length - 3);
    if (query.numero) {
        sql += "Limit ?";
        parametri.push(parseInt(query.numero));
    }
    console.log(sql, parametri);
    db.query(sql, parametri, (err, result) => {
        if (err) {
            res.json({
                success: false,
                result: { testo: "Errore" }
            });
            return;
        }
        res.json({
            success: true,
            result: { voltiTrovati: result },
        });
    });
});
var i = 0;
router.post("/add", multipartMiddleware, function(req, res, next) {
    let query = req.body;
    if (!req.files.immagine || !query.idDispositivo || !query.vettVolto) {
        res.json({ success: false, testo: "mancano parametri o sono errati" });
        return;
    }
    let p = req.files.immagine.path;
    let immagine = p.substring(p.lastIndexOf("/") + 1, p.length);
    let sql = "";
    let parametri = [];
    if (query.dataRilevazione) {
        sql = "INSERT into voltotrovato (DataRilevazione,Immagine,IdDispositivo,VettoreVolto) VALUES (?,?,?,?)";
        parametri = [query.dataRilevazione, immagine, query.idDispositivo, query.vettVolto];
    } else {
        sql =
            "INSERT into voltotrovato (Immagine,IdDispositiv,VettoreVolto) VALUES (?,?,?)";
        parametri = [immagine, query.idDispositivo, query.vettVolto];
    }

    db.query(sql, parametri,
        (err, result) => {
            if (err) {
                res.json({
                    success: false,
                    result: { testo: "Errore" }
                });
                return;
            }
            console.log(result);

            if (query.idVolto)
                db.query(
                    "INSERT into permesso (IdVoltoTrovato,IdVoltoRegistrato) VALUES (?,?)", [result.insertId, query.idVolto],
                    (err, result) => {
                        if (err) {
                            res.json({
                                success: false,
                                result: { testo: "Errore" }
                            });
                            return;
                        }
                        console.log(result);
                        res.json({
                            success: true,
                            result: { testo: "inserimento avvenuto con successo" },
                        });
                    }
                );
            else {
                sendMail(req.Utente.Username, req.Utente.Mail, "SCONOSCIUTO ENTRATO IN CASA", "<img src='http://80.22.36.186/" + req.Utente.Key + "/immagine/" + immagine + "'/>");
                res.json({
                    success: true,
                    result: { testo: "inserimento avvenuto con successo" },
                });
            }
        }
    );
});

const sendMail = (username, mailTo, subject, body) => {
    var mailOptions = {
        from: process.env.MAILUSER,
        to: `${username} ${mailTo}`,
        subject: subject,
        html: body,
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

module.exports = router;