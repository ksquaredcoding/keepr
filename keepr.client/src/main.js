import '@mdi/font/css/materialdesignicons.css'
import 'bootstrap'
import { createApp } from 'vue'
// @ts-ignore
import App from './App.vue'
import { registerGlobalComponents } from './registerGlobalComponents'
import { router } from './router'
import 'masonry-layout'
import 'vue-masonry'
import { VueMasonryPlugin } from "vue-masonry"
import mitt from "mitt"

const root = createApp(App)
registerGlobalComponents(root)
const emitter = mitt()

root
  .use(router)
  .use(VueMasonryPlugin)
  .mount('#app')
