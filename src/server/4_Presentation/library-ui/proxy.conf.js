module.exports = {
  "/api": {
    target:
      process.env["services__libraryapi__https__0"] ||
      process.env["services__libraryapi__http__0"],
    secure: process.env["NODE_ENV"] !== "development",
    "ws": true,
    pathRewrite: {
      "^/api": ""
    },
    "logLevel": "debug"
  }
};
