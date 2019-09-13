<template>
    <v-toolbar color="primary"
               fixed
               dark
               app>
        <v-toolbar-side-icon @click.stop="handleDrawerToggle"></v-toolbar-side-icon>
        <!--<v-text-field flat
                      solo-inverted
                      prepend-inner-icon="search"
                      label="Search"
                      class="hidden-sm-and-down">
        </v-text-field>-->
        <v-spacer></v-spacer>
        <v-btn icon>
            <v-icon>apps</v-icon>
        </v-btn>

        <v-btn icon>
            <v-icon>refresh</v-icon>
        </v-btn>

        <v-menu bottom left>
            <v-btn slot="activator"
                   dark
                   icon>
                <v-icon>more_vert</v-icon>
            </v-btn>

            <v-list>
                <v-list-tile v-for="(item, i) in items"
                             :key="i"
                             @click="">
                    <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                </v-list-tile>
            </v-list>
        </v-menu>
    </v-toolbar>
</template>
<script>

    export default {
        name: 'app-toolbar',
        data: () => ({
            items: [
                {
                    icon: 'account_circle',
                    href: '#',
                    title: 'Profile',
                    click: (e) => {
                        console.log(e);
                    }
                },
                {
                    icon: 'settings',
                    href: '#',
                    title: 'Settings',
                    click: (e) => {
                        console.log(e);
                    }
                },
                {
                    icon: 'fullscreen_exit',
                    href: '#',
                    title: 'Logout',
                    click: (e) => {
                        window.getApp.$emit('APP_LOGOUT');
                    }
                }
            ],
        }),
        computed: {
            toolbarColor() {
                return this.$vuetify.options.extra.mainNav;
            }
        },
        methods: {
            handleDrawerToggle() {
                window.getApp.$emit('APP_DRAWER_TOGGLED');
            },
        }
    };
</script>