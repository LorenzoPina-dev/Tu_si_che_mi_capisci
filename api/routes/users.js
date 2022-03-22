var express = require("express");
var router = express.Router();
const fs = require("fs");
var sha256 = require("js-sha256").sha256;
var multipart = require("connect-multiparty");
var multipartMiddleware = multipart({ uploadDir: "uploads/" });
var db = require("./../util/db");

router.get("/", function(req, res) {
    res.json({ seccess: true, utente: req.Utente });
});

router.post("/cambiaInfo", multipartMiddleware, function(req, res) {
    let query = req.body;
    if (query.length == 0) {
        res.json({ success: false, testo: "inserisci dei parametri" });
        return;
    }
    let sql = "UPDATE utente SET ",
        parametri = [];
    if (query.username != undefined) {
        sql += "Username=?, "
        parametri.push(query.username);
    }
    if (query.password != undefined) {
        sql += "Password=?, "
        parametri.push(sha256.hex(query.password));
    }

    if (query.mail != undefined) {
        sql += "EMail=?, "
        parametri.push(query.mail.split('.')[0] + ".png");
    }
    if (query.immagine != undefined) {
        sql += "Immagine=?, "
        fs.unlinkSync("uploads\\" + req.Utente.Id + ".png")
        fs.renameSync(
            req.files.Immagine.path,
            "uploads\\" + req.Utente.Id + ".png"
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
                result: { message: "update avvenuto con successo" }
            });
        });
});

module.exports = router;