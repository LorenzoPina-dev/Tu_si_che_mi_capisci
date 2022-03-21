var mysql = require("mysql");
var con = mysql.createPool({
    connectionLimit: process.env.LIMIT || 5,
    host: process.env.HOST || "localhost",
    user: process.env.USER || "root",
    password: process.env.PASSWORD || "",
    database: process.env.DB,
});
con.getConnection((err, connection) => {
    if (err) throw err;
    console.log("Database connected successfully");
    connection.release();
});
module.exports = con;