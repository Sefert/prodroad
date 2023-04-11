import axios from "axios";
//import { useI18n } from "vue-i18n";

export const httpClient = axios.create({
  baseURL: "https://localhost:7222/api/v1",
  headers: {
    "Content-type": "application/json",
  },
});

/*httpClient.interceptors.request.use((config) => {
  var i18n = useI18n();
  
  if (config.url != "https://localhost:7222/api/v1/Identity/Account/Login"){
    config.url += `?culture=${i18n.locale}`;
  }
      console.log(config.url);
  return config;
}, (error: AxiosError) => {
  return Promise.reject(error);
})
;*/

export default httpClient;
