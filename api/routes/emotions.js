var express = require("express");
var router = express.Router();
const fs = require("fs");
const db = require("../util/db")
var multipart = require("connect-multiparty");
var multipartMiddleware = multipart({ uploadDir: "uploads/" });
router.get("/", function(req, res) {
    let query = req.query;
    let sql =
        "SELECT emozionetrovata.* from (emozionetrovata join dispositivo on emozionetrovata.IdDispositivo=dispositivo.Id) WHERE dispositivo.IdUtente=? AND";
    let parametri = [req.Utente.Id];

    if (query.start || (query.data && new Date(query.data).getTime()) || query.tipo) {
        if (query.start) {
            sql += " emozionetrovata.Id>=? AND";
            parametri.push(parseInt(query.start));
        }
        if (query.data) {
            let date = (sql += " DataRilevazione>=? AND");
            parametri.push(query.data);
        }
        if (query.tipo) {
            sql += " IdEmozione=? AND";
            parametri.push(parseInt(query.tipo));
        }
    }
    sql = sql.substring(0, sql.length - 3)
    if (query.numero) {
        sql += " Limit ?"
        parametri.push(parseInt(query.numero));
    }
    console.log(sql, parametri);
    db.query(sql, parametri, (err, result) => {
        if (err) console.log(err);
        res.json({
            success: true,
            result: { emozioni: result },
        });
    });
});
var i = 0;
router.post("/add", function(req, res, next) {
    let query = req.body;
    console.log(query)
    if (!query.tipo || !query.dataRilevazione || !query.ora || !query.idDispositivo) {
        res.json({ success: false, result:{ testo: "mancano parametri o sono errati" }});
        return;
    }
    db.query("INSERT into emozionetrovata (IdEmozione,DataRilevazione,Ora,IdDispositivo) VALUES (?,?,?,?)", [query.tipo, query.dataRilevazione, query.ora, query.idDispositivo], (err, result) => {
        if (err) console.log(err);
        res.json({
            success: true,
            result: { testo: "inserimento avvenuto con successo" },
        });
    });
});

module.exports = router;