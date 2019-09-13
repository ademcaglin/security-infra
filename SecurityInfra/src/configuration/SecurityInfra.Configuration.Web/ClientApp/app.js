import Vue from 'vue';
import VueRouter from 'vue-router';
import routes from './routes';
import 'vuetify/dist/vuetify.min.css';
import 'font-awesome/css/font-awesome.css';
import 'material-design-icons-iconfont/dist/material-design-icons.css';
import 'vuetify/src/stylus/app.styl';
import './theme.styl';
import Vuetify from 'vuetify';
import tr from './tr';
import App from './App.vue';

if (module.hot) {
    module.hot.accept();
}
Vue.use(VueRouter);
Vue.use(Vuetify, {
    lang: {
        locales: { tr },
        current: 'tr'
    }
});
const router = new VueRouter({
    routes: routes,
    mode: 'history'
});
Vue.config.productionTip = false;
new Vue({
    el: '#app',
    mode: 'history',
    router,
    render: h => h(App)
});