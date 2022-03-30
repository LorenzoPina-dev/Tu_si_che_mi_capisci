var mysql = require("mysql2");
var con = mysql.createConnection({
    host: process.env.HOST || "localhost",
    user: process.env.USER || "root",
    password: process.env.PASSWORD || "",
    database: process.env.DB,
});
module.exports = con;