const path = require('path')
const HtmlWebpackPlugin = require('html-webpack-plugin')
const { Template } = require('webpack')

module.exports = {
    mode: 'production',
    entry: './src/index',  
    output: {
        path: path.resolve(__dirname, 'dist'), /*current directory with joined dist*/
        filename: "app.js" /*takes name from entry and makse js file */
    },
    resolve: {
      extensions: ['.ts', '.tsx', '.js', '.json', '.css']
    },
    plugins : [
        new HtmlWebpackPlugin(
            {
                template : "./src/index.html",
                inject : "body",/*inject into body tag*/
                minify : false,
            }
        )
    ],
    /*https://www.npmjs.com/package/css-loader*/
    module: {
        rules: [
          {
            test: /\.css$/i,
            use: ["style-loader", "css-loader"],
            exclude: /\.module\.css$/,
          },
          {
            test: /\.(ts|js)x?$/,
            use: 'babel-loader',
            exclude: /node_modules/,
          },
        ],
      },
}