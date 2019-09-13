const webpack = require('webpack');
const path = require('path');

module.exports = {
    watchOptions: {
        aggregateTimeout: 300,
        poll: 1000
    },
    entry: {
        "react": ['react', 'react-dom', '@material-ui/core', "axios"],
        "tenantadmin": "./ClientApp/TenantAdmin/index.js"
    },
    output: {
        path: path.join(__dirname, 'wwwroot', 'dist'),
        filename: '[name].js',
        publicPath: '/dist/'
    },
    resolve: {
        extensions: ['.js', '.jsx']
    },
    optimization: {
        splitChunks: {
            cacheGroups: {
                react: {
                    chunks: 'initial',
                    name: 'react',
                    test: 'react',
                    enforce: true
                }
            }
        },
        runtimeChunk: false
    },
    module: {
        rules: [
            {
                test: /\.(js|jsx)$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-env', '@babel/preset-react']
                    }
                }
            }
        ]
    }
};