const webpack = require('webpack');
const path = require('path');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const CleanWebpackPlugin = require('clean-webpack-plugin');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const { VueLoaderPlugin } = require('vue-loader');
const MonocoEditorPlugin = require('monaco-editor-webpack-plugin');

module.exports = {
    watchOptions: {
        aggregateTimeout: 300,
        poll: 1000
    },
    entry: {
        "app": "./ClientApp/app.js",
        "commons": ["vue",
            "vue-router",
            "vuetify",
            "vuetify/dist/vuetify.min.css",
            "vuetify/src/stylus/app.styl",
            "font-awesome/css/font-awesome.css",
            "material-design-icons-iconfont/dist/material-design-icons.css",
            "axios",
            "vue-monaco"
        ]
    },
    optimization: {
        splitChunks: {
            cacheGroups: {
                commons: {
                    name: 'commons',
                    chunks: 'initial',
                    minChunks: 2
                }
            }
        },
        runtimeChunk: false
    },
    output: {
        path: path.join(__dirname, 'wwwroot', 'dist'),
        filename: '[name].js',
        publicPath: '/dist/'
    },
    resolve: {
        extensions: ['.js', '.jsx']
    },
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: 'vue-loader',
                options: {
                    loaders: {
                        js: 'babel-loader'
                    },
                    plugins: ["transform-object-rest-spread"]
                }
            },
            {
                test: /\.js$/,
                exclude: /(node_modules|bower_components)/,
                use: [{
                    loader: "babel-loader",
                    options: { presets: ['es2015'], plugins: ["transform-object-rest-spread"] }
                }]
            },
            {
                test: /\.(sa|sc|c)ss$/,
                use: [
                    MiniCssExtractPlugin.loader,
                    'css-loader',
                    'sass-loader'
                ]
            },
            {
                test: /\.styl$/,
                loader: 'css-loader!stylus-loader?paths=node_modules/bootstrap-stylus/stylus/'
            },
            {
                test: /\.(png|woff|woff2|eot|ttf|svg)$/,
                use: 'url-loader?limit=100000'
            },
            {
                test: /\.html$/,
                use: 'html-loader'
            }
        ]
    },
    plugins: [
        new CleanWebpackPlugin(['./wwwroot/']),
        //new CopyWebpackPlugin([{
        //    from: './ClientApp/assets/images/*.*',
        //    to: "img/",
        //    flatten: true
        //}]),
        new MiniCssExtractPlugin({
            filename: "[name].css"
        }),
        new VueLoaderPlugin(),
        new MonocoEditorPlugin({
            languages: ['json']
        })
    ]
};

