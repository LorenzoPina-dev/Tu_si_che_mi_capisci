var express = require("express");
var router = express.Router();
const fs = require("fs");
var multipart = require("connect-multiparty");
var multipartMiddleware = multipart({ uploadDir: "uploads/" });
var db = require("./../util/db");
router.get("/", function(req, res) {
    res.header("Content-type", "application/json");
    res.end("{seccess:true}");
});

router.get("/cambiaInfo", function(req, res, next) {
    console.log(req.params);
    db.query(
        "UPDATE utente SET Username=?, Password=?, Email=?, Immagine=? WHERE ApiKey = ?", [req.query.username,req.query.password,req.query.email,req.query.immagine, req.params.key],
        (err, result) => {
            if (err) throw err;
            res.json({
                success: true,
                result: { message:"update avvenuto con successo" }
            });
        }
    );
});

module.exports = router;