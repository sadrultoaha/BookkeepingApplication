import { createRouter, createWebHistory } from 'vue-router'
import Layout from "../layouts/Layout.vue"

const routes = [
  {
    path: '/',
    component: Layout,
    children: [
      {
        path: '/',
        name: 'Dashboard',
        component: () => import("../views/Dashboard.vue")
      },
      {
        path: '/record-types',
        name: 'Types',
        component: () => import("../views/RecordTypes.vue")
      },
      {
        path: '/income',
        name: 'Income',
        component: () => import("../views/Income.vue")
      },
      {
        path: '/expense',
        name: 'Expense',
        component: () => import("../views/Expense.vue")
      },
      {
        path: '/reconciliation',
        name: 'Reconciliation',
        component: () => import("../views/Reconciliation.vue")
      }
    ]
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
