var express = require("express");
var router = express.Router();
const fs = require("fs");
const db = require("../util/db");
var multipart = require("connect-multiparty");
var multipartMiddleware = multipart({ uploadDir: "uploads/facceRegistrate/" });
router.get("/", function(req, res) {
    let query = req.query;
    let sql = "SELECT * from voltoregistrato where IdUtente=? AND";
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
        sql += " Limit ?";
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
            result: { voltiRegistrati: result },
        });
    });
});
var i = 0;
router.post("/add", multipartMiddleware, function(req, res, next) {
    let query = req.body;
    if (!query.nome || !query.immagine) {
        res.json({ success: false, testo: "mancano parametri o sono errati" });
        return;
    }
    db.query(
        "INSERT into voltoregistrato (Nome,Immagine,IdUtente) VALUES (?,?,?)", [query.nome, query.immagine, req.Utente.Id],
        (err, result) => {
            if (err) {
                res.json({
                    success: false,
                    result: { testo: "Errore" }
                });
                return;
            }
            fs.renameSync(
                req.files.Immagine.path,
                "uploads\\voltoregistrato\\" + result.insertId + ".png"
            );
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
    db.query(
        "Select IdUtente FrOM voltoregistrato WHERE Id=?", [req.params.Id],
        (err, result) => {
            if (err) {
                res.json({
                    success: false,
                    result: { testo: "Errore" }
                });
                return;
            }
            if (result[0].IdUtente == req.Utente.Id) {
                db.query(
                    "DELETE FrOM voltoregistrato WHERE Id=?", [req.params.Id],
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
                            result: { testo: "rimozione avvenuta con successo" },
                        });
                    }
                );
            }
        }
    );
});

module.exports = router;