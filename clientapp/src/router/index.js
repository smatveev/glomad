import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
import Country from '../views/Country.vue'
import Embassy from '../views/Embassy.vue'



const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/:name',
    name: "Country",
    component: Country
  },
  {
    path: '/:name/Embassy/:name',
    name: "Embassy",
    component: Embassy
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
