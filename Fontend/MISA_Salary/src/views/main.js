import { createApp } from "vue";
import App from "./App.vue";
import i18n from "../locales";
import router from "../router";
// Import icon CSS
import "@/assets/styles/main.css";
// Import DevExtreme CSS
import "devextreme/dist/css/dx.light.css";
import "devextreme/dist/css/dx.common.css";
import globalComponent from "@/commons/globalComponent.js";
const app = createApp(App);
globalComponent.register(app);
app.use(i18n);
app.use(router);
app.mount("#app");
