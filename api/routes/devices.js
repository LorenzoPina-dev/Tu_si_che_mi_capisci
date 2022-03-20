var express = require("express");
var router = express.Router();
const fs = require("fs");
var multipart = require("connect-multiparty");
var multipartMiddleware = multipart({ uploadDir: "uploads/" });
router.get("/", function(req, res) {
    res.header("Content-type", "application/json");
    res.end("{seccess:true}");
});
var i = 0;
router.post("/add", multipartMiddleware, function(req, res, next) {
    console.log(req.files.img);
    fs.renameSync(
        req.files.img.path,
        "uploads\\" + i++ + "." + req.files.img.path.split(".")[1]
    );
    res.json({ message: "Successfully uploaded files" });
});

router.post("/remove", multipartMiddleware, function(req, res, next) {
    console.log(req.files.img);
    fs.renameSync(
        req.files.img.path,
        "uploads\\" + i++ + "." + req.files.img.path.split(".")[1]
    );
    res.json({ message: "Successfully uploaded files" });
});

module.exports = router;