var express = require('express');
var router = express.Router();
const fs = require('fs');
/*const multer = require("multer");
const upload = multer({ dest: "uploads/" });*/
var multipart = require("connect-multiparty");
var multipartMiddleware = multipart({ uploadDir: "uploads/" });
router.get('/', function(req, res) {
    res.header('Content-type', 'application/json');
    res.end("{seccess:true}")
})
var i = 0;
/* GET home page. */
router.post("/", multipartMiddleware, function(req, res, next) {
    //res.render('index', { title: 'Express' });
    console.log(req.files.img);
    fs.renameSync(req.files.img.path, 'uploads\\' + (i++) + "." + req.files.img.path.split('.')[1]);
    res.json({ message: "Successfully uploaded files" });
    /*let ob = new Object();
    ob.title = 'Express';
    ob.success = true;
    ob.nome = "Pippo";
    res.header('Content-Type', 'application/json');
    res.send(JSON.stringify(ob))*/
});

module.exports = router;