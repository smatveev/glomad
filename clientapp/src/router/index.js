import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
import Country from '../views/Country.vue'
import Embassy from '../views/Embassy.vue'
import Countries from '../views/Countries.vue'



const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
    meta: {
      title: 'Home Page - Glomad',
      metaTags: [
        {
          name: 'description',
          content: 'Welcome to digital nomads home.'
        },
        {
          property: 'og:description',
          content: 'Welcome to digital nomads home.'
        }
      ]
    }
  },
  {
    path: '/:name',
    name: "Country",
    component: Country
  },
  {
    path: '/countries',
    name: "Countries",
    component: Countries,
    meta: {
      title: 'Countries list - Glomad',
      metaTags: [
        {
          name: 'description',
          content: 'Latest covid and travel data all countries.'
        },
        {
          property: 'og:description',
          content: 'Latest covid and travel data all countries.'
        }
      ]
    }
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
