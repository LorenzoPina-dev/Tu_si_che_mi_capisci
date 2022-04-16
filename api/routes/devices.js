var express = require("express");
var router = express.Router();
const fs = require("fs");
const db = require("../util/db");
router.get("/", function(req, res) {
    let query = req.query;
    let sql = "SELECT * from dispositivo where IdUtente=? AND";
    let parametri = [req.Utente.Id];

    if (query.start) {
        sql += " Id>=? AND";
        parametri.push(parseInt(query.start));
    }
    if (query.tipo) {
        let date = (sql += " Tipo=? AND");
        parametri.push(query.tipo);
    }
    sql = sql.substring(0, sql.length - 3);
    if (query.numero) {
        sql += " Limit ?";
        parametri.push(parseInt(query.numero));
    }
    db.query(sql, parametri, (err, result) => {
        if (err) {
            res.json({
                success: false,
                result: { testo: "Errore" },
            });
            return;
        }
        res.json({
            success: true,
            result: { dispositivo: result },
        });
    });
});
var i = 0;
router.post("/add", function(req, res, next) {
    let query = req.body;
    if (!query.nome || !query.tipo || !query.ip) {
        res.json({ success: false, testo: "mancano parametri o sono errati" });
        return;
    }
    let sql = "";
    let parametri = [];
    if (query.acceso) {
        sql =
            "INSERT into dispositivo (Nome,Tipo,Ip,Acceso,IdUtente) VALUES (?,?,?,?,?)";
        parametri = [query.nome, query.tipo, query.ip, query.acceso, req.Utente.Id];
    } else {
        sql =
            "INSERT into dispositivo (Nome,Tipo,Ip,IdUtente) VALUES (?,?,?,?,?)";
        parametri = [query.nome, query.tipo, query.ip, req.Utente.Id];
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
            res.json({
                success: true,
                result: { testo: "inserimento avvenuto con successo" }
            });
        }
    );
});
router.delete("/remove/:id", function(req, res, next) {
    if (!req.params.id) {
        res.json({ success: false, testo: "mancano parametri o sono errati" });
        return;
    }
    db.query(
        "Select IdUtente FrOM dispositivo WHERE Id=?", [req.params.Id],
        (err, result) => {
            if (err) {
                res.json({
                    success: false,
                    result: { testo: "Errore" },
                });
                return;
            }
            if (result[0].IdUtente == req.Utente.Id) {
                db.query(
                    "DELETE FrOM dispositivo WHERE Id=?", [req.params.Id],
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
                            result: { testo: "rimozione avvenuto con successo" },
                        });
                    }
                );
            }
        }
    );
});

module.exports = router;
