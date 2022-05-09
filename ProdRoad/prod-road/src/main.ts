import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { createI18n } from 'vue-i18n'

import en from './locales/en.json'
import et from './locales/et.json'

import App from '@/App.vue'
import router from './router'

//import 'jquery';
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';

const i18n = createI18n({
    legacy: false,
    locale: 'en', // set locale
    fallbackLocale: 'en', // set fallback locale
    availableLocales: ['en', 'et'],
    globalInjection: true,

    messages: {
        en,
        et
    }
})

const app = createApp(App)

app.use(i18n)
app.use(createPinia())
app.use(router)

app.mount('#app')
