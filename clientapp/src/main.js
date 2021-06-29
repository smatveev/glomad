import 'bootstrap/dist/css/bootstrap.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

import FlashMessage from "@smartweb/vue-flash-message";


const app = createApp(App)

app.use(FlashMessage, {tag: 'flash-message', strategy: 'multiple'})
app.use(router)

app.mount('#app')
