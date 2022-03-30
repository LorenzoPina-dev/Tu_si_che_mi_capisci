var express = require("express");
var router = express.Router();
const fs = require("fs");
var db = require("./../util/db");
const path = require("path");
router.get("/:tabella/:nomeFile", (req, res) => {
    let tabella = req.params.tabella;
    if (!(tabella == "obiettivo" || tabella == "utente" || tabella == "voltoregistrato" || tabella == "voltotrovato")) {
        res.json({
            success: false,
            result: { testo: "patametri errati" },
        });
        return;
    }
    db.query(
        `SELECT * from ${tabella} where Immagine=?`, [req.params.nomeFile],
        (err, result) => {
            if (err) {
                res.json({
                    success: false,
                    result: { testo: "Errore" }
                });
                return;
            }
            let filePath = path.join(
                __dirname,
                "../uploads/" + req.params.tabella + "/" + req.params.nomeFile
            );
            if (result.length > 0 && result[0].IdUtente == req.Utente.Id && fs.existsSync(filePath)) {
                res.sendFile(filePath);
            } else
                res.status(404).json({ success: false, result: { testo: "file non trovato" } });
        }
    );
});

module.exports = router;