var express = require("express");
var router = express.Router();
const fs = require("fs");
const db = require("../util/db")
var multipart = require("connect-multiparty");
var multipartMiddleware = multipart({ uploadDir: "uploads/" });
router.get("/", function(req, res) {
    db.query("SELECT * from emozionetrovata", [], (err, result) => {
        if (err) console.log(err);
        res.json({
            success: true,
            result: { emozioni: result },
        });
    });
});
var i = 0;
router.post("/", multipartMiddleware, function(req, res, next) {

    console.log(req.files.img);
    fs.renameSync(
        req.files.img.path,
        "uploads\\" + i++ + "." + req.files.img.path.split(".")[1]
    );
    res.json({ message: "Successfully uploaded files" });
});

module.exports = router;