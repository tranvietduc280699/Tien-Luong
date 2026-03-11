import { createRouter, createWebHistory } from "vue-router";
import MainLayout from "@/layouts/MainLayout.vue";
import SalaryComposition from "@/views/Salarys/SalaryComposition.vue";
import AddSalary from "@/views/Salarys/AddSalary.vue";
import SalarySystem from "@/views/Salarys/SalarySystem.vue";
import FeatureDevelopment from "@/views/FeatureDevelopment/FeatureDevelopment.vue";
const routers = [
  {
    path: "/",
    redirect: "/salarycomposition",
    component: MainLayout,
    children: [
      {
        path: "salarycomposition",
        name: "SalaryComposition",
        component: SalaryComposition,
      },
      {
        path: `add-salary/:id?`,
        name: "AddSalary",
        component: AddSalary,
      },
      {
        path:`salary-system`,
        name:"SalarySystem",
        component: SalarySystem,
      },
      {
        path:"featureDevelopment",
        name:"FeatureDevelopment",
        component:FeatureDevelopment,
      }
    ],
  },
];
const router = createRouter({
  history: createWebHistory(),
  routes: routers,
});
export default router;
