<template>
    <div id="appRoot">
            <v-app class="app">
                <app-menu class="app--drawer"></app-menu>
                <app-toolbar class="app--toolbar"></app-toolbar>
                <v-content>
                    <div class="page-wrapper">
                        <router-view></router-view>
                    </div>
                </v-content>
                <!-- Go to top -->
                <!--<app-fab></app-fab>-->
               
            </v-app>
    </div>
</template>
<script>
    import AppMenu from './components/layout/AppMenu.vue';
    import AppToolbar from './components/layout/AppToolbar.vue';
    export default {
        components: {
            AppMenu,
            AppToolbar
        },
        data: () => ({
            expanded: true,
            rightDrawer: false,
            snackbar: {
                show: false,
                text: '',
                color: '',
            }
        }),
        computed: {
        },
        created() {
            window.getApp = this;
        },
        methods: {
            openThemeSettings() {
                this.$vuetify.goTo(0);
                this.rightDrawer = (!this.rightDrawer);
            }
        },
    };
</script>


<style lang="css" scoped>
    .setting-fab {
        top: 50% !important;
        right: 0;
        border-radius: 0
    }
    .page-wrapper {
        min-height: calc(100vh - 64px - 50px - 81px );
    }
</style>
