const Menu = [
    { header: 'Configuration' },
    {
        title: 'Clients',
        group: 'apps',
        icon: 'dashboard',
        name: 'Clients'
    },
    {
        title: 'Api Resources',
        group: 'apps',
        name: 'ApiResources',
        icon: 'view_module'
    },
    {
        title: 'Identity Resources',
        group: 'apps',
        name: 'IdentityResources',
        icon: 'view_module'
    },
    {
        title: 'Menu Providers',
        group: 'apps',
        icon: 'view_module',
        name: 'MenuProviders'
    }
];
// reorder menu
Menu.forEach((item) => {
    if (item.items) {
        item.items.sort((x, y) => {
            let textA = x.title.toUpperCase();
            let textB = y.title.toUpperCase();
            return (textA < textB) ? -1 : (textA > textB) ? 1 : 0;
        });
    }
});

export default Menu;