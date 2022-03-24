var express = require("express");
var router = express.Router();
const fs = require("fs");
var sha256 = require("js-sha256").sha256;
var multipart = require("connect-multiparty");
var multipartMiddleware = multipart({ uploadDir: "uploads/utenti" });
var db = require("./../util/db");

router.get("/", function(req, res) {
    if(!req.Utente)
        res.json({ success: false, result:{testo:"utente non trovato"} });
    else
        res.json({ success: true, result:{utente: req.Utente} });
});

router.put("/cambiaInfo", multipartMiddleware, function(req, res) {
    let query = req.body;
    if (!query.username && !query.password && !query.mail && !query.password && !query.immagine) {
        res.json({ success: false, testo: "inserisci dei parametri" });
        return;
    }
    let sql = "UPDATE utente SET ",
        parametri = [];
    if (query.username ) {
        sql += "Username=?, "
        parametri.push(query.username);
    }
    if (query.password) {
        sql += "Password=?, "
        parametri.push(sha256.hex(query.password));
    }

    if (query.mail ) {
        sql += "EMail=?, "
        parametri.push(query.mail.split('.')[0] + ".png");
    }
    if (query.immagine) {
        sql += "Immagine=?, "
        if(fs.existsSync("uploads\\utenti\\" + req.Utente.Id + ".png"))
            fs.unlinkSync("uploads\\utenti\\" + req.Utente.Id + ".png")
        fs.renameSync(
            req.files.Immagine.path,
            "uploads\\utenti\\" + req.Utente.Id + ".png"
        );
        parametri.push(query.immagine);
    }
    if (sql.substring(sql.length - 2, sql.length) == ", ")
        sql = sql.substring(0, sql.length - 2);
    parametri.push(req.Utente.Id);
    db.query(sql + " WHERE Id = ? ", parametri,
        (err, result) => {
            if (err)
                console.log(err)
            res.json({
                success: true,
                result: { testo: "update avvenuto con successo" }
            });
        });
});

module.exports = router;