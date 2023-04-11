import { createApp } from "vue";
import { createPinia } from "pinia";
import { createI18n } from "vue-i18n";
import { createVfm } from "vue-final-modal";

import en from "./locales/en-GB.json";
import et from "./locales/et-EE.json";

import App from "@/App.vue";
import router from "./router";

import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";

const i18n = createI18n({
  legacy: false,
  locale: getLocale(), // set locale
  fallbackLocale: "en-GB", // set fallback locale
  availableLocales: ["en-GB", "et-EE"],
  globalInjection: true,

  messages: {
    "en-GB": en,
    "et-EE": et,
  },
});

function getLocale(): string {
  const prodRoadLocale = window.localStorage.getItem("prodRoad-locale");
  if (prodRoadLocale == null || typeof prodRoadLocale == "undefined") {
    window.localStorage.setItem("prodRoad-locale", "en-GB");
  }
  return prodRoadLocale != null && typeof prodRoadLocale != "undefined"
    ? prodRoadLocale
    : "undefined";
  //prodRoadLocale: "en-GB";
}

const app = createApp(App);

app.use(createVfm);
app.use(i18n);
app.use(createPinia());
app.use(router);

app.mount("#app");
