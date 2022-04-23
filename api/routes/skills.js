var express = require("express");
var router = express.Router();
const fs = require("fs");
const db = require("../util/db")
router.get("/", function(req, res) {
    let query = req.query;
    let sql = "SELECT * from skill where IdUtente=? AND";
    let parametri = [req.Utente.Id];

    if (query.start) {
        sql += " Id>=? AND";
        parametri.push(parseInt(query.start));
    }
    sql = sql.substring(0, sql.length - 3)
    if (query.numero) {
        sql += " Limit ?"
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
            result: { skill: result },
        });
    });
});
var i = 0;
router.post("/add", function(req, res, next) {
    let query = req.body;
    if (!query.nome || !query.descrizione || !query.azione || !query.idEmozione) {
        res.json({ success: false, testo: "mancano parametri o sono errati" });
        return;
    }
    db.query(
        "INSERT into skill (Nome,Descrizione,Azione,IdEmozione,IdUtente) VALUES (?,?,?,?,?)", [query.nome, query.descrizione, query.azione, query.idEmozione, req.Utente.Id],
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
                result: { testo: "inserimento avvenuto con successo" },
            });
        }
    );
});
router.delete("/remove/:id", function(req, res, next) {
    if (!req.params.id) {
        res.json({ success: false, testo: "mancano parametri o sono errati" });
        return;
    }
    db.query("Select IdUtente FrOM skill WHERE Id=?", [req.params.id], (err, result) => {
        if (err) {
            res.json({
                success: false,
                result: { testo: "Errore" }
            });
            return;
        }
        if (result[0].IdUtente == req.Utente.Id) {
            db.query(
                "DELETE FrOM skill WHERE Id=?", [req.params.id],
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
                        result: { testo: "rimozione avvenuto con successo" },
                    });
                }
            );
        }
    });

});

module.exports = router;
