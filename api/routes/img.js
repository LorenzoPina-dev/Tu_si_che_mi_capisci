var express = require("express");
var router = express.Router();
const fs = require("fs");
var db = require("./../util/db");
const path = require("path");
router.get("/", (req, res) => {
    res.json({ success: false, result: { testo: "file non trovato" } });
});
router.get("/:tabella", (req, res) => {
    let tabella = req.params.tabella;
    let file = req.query.nomefile;
console.log(tabella+";"+file);    
if (!(tabella == "obiettivo" || tabella == "utente" || tabella == "voltoregistrato" || tabella == "voltotrovato")) {
        res.json({
            success: false,
            result: { testo: "patametri errati" }
        });
        return;
    }
	console.log(tabella+";"+file);
    db.query(
        `SELECT * from ${tabella} where Immagine=?`, [file],
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
                "../uploads/" + tabella + "/" + file
            );
            console.log(filePath);
            if (result.length > 0 && tabella == "utente" ? result[0].Id : result[0].IdUtente == req.Utente.Id && fs.existsSync(filePath)) {
                res.sendFile(filePath);
            } else
                res.status(404).json({ success: false, result: { testo: "file non trovato" } });
        }
    );
});

module.exports = router;
