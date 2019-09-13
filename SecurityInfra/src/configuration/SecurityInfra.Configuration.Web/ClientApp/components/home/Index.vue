<template>
    <monaco-editor class="editor" v-model="code" language="json" theme="vs-dark">
    </monaco-editor>
</template>

<script>
    import MonacoEditor from 'vue-monaco'

    export default {
        name: 'home',
        components: {
            MonacoEditor
        },

        data() {
            return {
                code: '{"aa":"bb"}'
            }
        }
    }
</script>

<style>
    .editor {
        margin: 20px;
        width: 80%;
        height: 600px;
    }
</style>