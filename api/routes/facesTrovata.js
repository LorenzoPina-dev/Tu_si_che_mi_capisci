var express = require("express");
var router = express.Router();
const fs = require("fs");
const db = require("../util/db");
var multipart = require("connect-multiparty");
var multipartMiddleware = multipart({ uploadDir: "uploads/facesTrovate/" });
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
        if (err) console.log(err);
        res.json({
            success: true,
            result: { voltiTrovati: result },
        });
    });
});
var i = 0;
router.post("/add", multipartMiddleware, function(req, res, next) {
    let query = req.body;
    if (!query.dataRilevazione || !query.dataRilevazione || !query.immagine || !query.idDispositivo) {
        res.json({ success: false, testo: "mancano parametri o sono errati" });
        return;
    }
        
    db.query(
        "INSERT into voltotrovato (DataRilevazione,Ora,Immagine,IdDispositivo) VALUES (?,?,?,?)", [query.dataRilevazione, query.ora, query.immagine, query.idDispositivo],
        (err, result) => {
            if (err) console.log(err);
            console.log(result);
            
            if(query.idVolto)
                db.query(
                    "INSERT into permesso (IdVoltoTrovato,IdVoltoRegistrato) VALUES (?,?)", [result.insertId, query.idVolto],
                    (err, result) => {
                        if (err) console.log(err);
                        console.log(result);
                        res.json({
                            success: true,
                            result: { testo: "inserimento avvenuto con successo" },
                        });
                    }
                );
            else
            res.json({
                success: true,
                result: { testo: "inserimento avvenuto con successo" },
            });
        }
    );
});

module.exports = router;