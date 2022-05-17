import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import VueToast from 'vue-toast-notification';
import 'vue-toast-notification/dist/theme-sugar.css';
import "@vueform/multiselect/themes/default.css";

//createApp(App).use(router).mount('#app')

const app = createApp(App);

app.use(VueToast, {
    position: 'top',
    queue: false,
    duration:30000,
    dismissible: true
});
app.use(router);
app.mount('#app');
