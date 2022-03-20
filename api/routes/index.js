var express = require('express');
var router = express.Router();
const fs = require('fs');
var multipart = require("connect-multiparty");
var multipartMiddleware = multipart({ uploadDir: "uploads/" });
const crypto = require("crypto");
const sha256 = crypto.createHash('sha256');
/*router.get('/', function(req, res) {
    res.header('Content-type', 'application/json');
    res.end("{seccess:true}")
})
var i = 0;
router.post("/", multipartMiddleware, function(req, res, next) {
    console.log(req.files.img);
    fs.renameSync(req.files.img.path, 'uploads\\' + (i++) + "." + req.files.img.path.split('.')[1]);
    res.json({ message: "Successfully uploaded files" });
});*/
router.get("/login", (req, res) => {
    let db = require("./../util/db");
    console.log(req.query);
    db.query(
        "SELECT * FROM utente WHERE Username = ? AND Password = ?", [req.query.username, req.query.password],
        (err, result) => {
            if (err) throw err;
            console.log(result);
            db.destroy();
            res.json({ success: true, result: { Id: result[0].Id, ApiKey: result[0].ApiKey } });
        }
    );
});

router.get("/register", (req, res) => {
    let db = require("./../util/db");
    let query = req.query;
    let d = new Date().getTime();
    key = "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function(c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == "x" ? r : (r & 0x3) | 0x8).toString(16);
    });
    let criptata = sha256.update(query.password, "utf8").digest("base64");
    console.log(criptata);
    db.query(
        "INSERT INTO utente (Username, Password, Email, Immagine, ApiKey) VALUES (?,?,?,?,?)", [query.username, criptata, query.email, query.immagine, key],
        (err, result) => {
            if (err) throw err;
            console.log(result);
            db.destroy();
            res.json({
                success: true,
            });
        }
    );
});
module.exports = router;