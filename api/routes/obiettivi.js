var express = require("express");
var router = express.Router();
const fs = require("fs");
const db = require("../util/db");
router.get("/", function(req, res) {
    let query = req.query;
    let sql =
        "SELECT obiettivo.*,quest.Concluso from (obiettivo join quest on quest.IdObiettivo=obiettivo.Id) WHERE quest.IdUtente=? AND";
    let parametri = [req.Utente.Id];

    if (query.start) {
        sql += " emozionetrovata.Id>=? AND";
        parametri.push(parseInt(query.start));
    }
    sql = sql.substring(0, sql.length - 3);
    if (query.numero) {
        sql += " Limit ?";
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
    console.log(query);
    if (!query.concluso || !query.idObiettivo) {
        res.json({
            success: false,
            result: { testo: "mancano parametri o sono errati" },
        });
        return;
    }
    db.query(
        "INSERT into quest (Concluso,IdObiettivo,IdUtente) VALUES (?,?,?)", [query.concluso, query.idObiettivo, req.Utente.Id],
        (err, result) => {
            if (err) console.log(err);
            res.json({
                success: true,
                result: { testo: "inserimento avvenuto con successo" },
            });
        }
    );
});

router.post("/change/:id", function(req, res, next) {
    let query = req.body;
    console.log(query);
    if (!req.params.id || !query.concluso) {
        res.json({
            success: false,
            result: { testo: "mancano parametri o sono errati" },
        });
        return;
    }
    db.query(
        "Select IdUtente FrOM query WHERE Id=?", [req.params.Id],
        (err, result) => {
            if (err) console.log(err);
            if (result[0].IdUtente == req.Utente.Id) {
                db.query(
                    "update query set Concluso=? Where Id=?", [req.params.id, query.concluso],
                    (err, result) => {
                        if (err) console.log(err);
                        res.json({
                            success: true,
                            result: { testo: "inserimento avvenuto con successo" },
                        });
                    }
                );
            }
        }
    );
});

module.exports = router;