import 'bootstrap/dist/css/bootstrap.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

import VueYandexMetrika from 'vue-yandex-metrika'   

//import FlashMessage from "@smartweb/vue-flash-message";
import FlagIcon from 'vue-flag-icon'


const app = createApp(App)

//app.use(FlashMessage, {tag: 'flash-message', strategy: 'multiple'})
app.use(router);

app.use(FlagIcon);

app.use(VueYandexMetrika, {
    id: 56660788,
    router: router,
    env: process.env.NODE_ENV,
    options: {        
        clickmap:true,
        trackLinks:true,
        accurateTrackBounce:true,
        webvisor:true
    }
    // other options
});

app.mount('#app')
