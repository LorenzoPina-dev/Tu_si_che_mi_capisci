

var express = require("express");
var router = express.Router();
const fs = require("fs");
const path = require("path");
var sha256 = require("js-sha256").sha256;
var multipart = require("connect-multiparty");
            let filePath = path.join(
                __dirname,
                "../uploads/utente"
            );
var multipartMiddleware = multipart({ uploadDir:filePath });
var db = require("./../util/db");

router.get("/", function(req, res) {
    if(!req.Utente)
        res.json({ success: false, result:{testo:"utente non trovato"} });
    else
        res.json({ success: true, result:{utente: req.Utente} });
});

router.put("/cambiaInfo", multipartMiddleware, function(req, res) {
    let query = req.body;
	console.log("ciao");
    if (!query.username && !query.mail && !query.password && !query.immagine) {
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
        parametri.push(query.mail);
    }
	console.log(req.files);
    if (req.files.immagine) {

        sql += "Immagine=?, ";
	let p=req.files.immagine.path;
 console.log(p.substring(p.lastIndexOf('/')+1,p.length)); 
	parametri.push(p.substring(p.lastIndexOf('/')+1,p.length));
    }
	console.log(sql);
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
