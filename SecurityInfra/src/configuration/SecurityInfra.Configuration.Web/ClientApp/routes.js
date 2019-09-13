import Home from "./components/home/Index.vue";
import Clients from "./components/clients/Index.vue";
import IdentityResources from "./components/identityresources/Index.vue";
import ApiResources from "./components/apiresources/Index.vue";
import MenuProviders from "./components/menuproviders/Index.vue";

let routes = [
    {
        path: '/',
        component: Home,
        name: 'Home'
    },
    {
        path: '/clients',
        component: Clients,
        name: 'Clients'
    },
    {
        path: '/identityresources',
        component: IdentityResources,
        name: 'IdentityResources'
    },
    {
        path: '/apiresources',
        component: ApiResources,
        name: 'ApiResources'
    },
    {
        path: '/menuproviders',
        component: MenuProviders,
        name: 'MenuProviders'
    },
    {
        path: '*',
        hidden: true,
        redirect: { path: '/404' }
    }
];

export default routes;
