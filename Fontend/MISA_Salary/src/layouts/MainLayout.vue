<template>
  <div class="main-container">
    <Header />
    <div class="body-layout">
      <Sidebar />
      <!-- <content /> -->
      <router-view></router-view>
      <NotificationToast
        v-if="state.showToast"
        :toastSuccess="state.toastSuccess"
        :toastError="state.toastError"
        :toastWarning="state.toastWarning"
      />
    </div>
  </div>
</template>
<script setup>
import Header from "@/components/Header.vue";
import Sidebar from "@/components/Sidebar.vue";
//import Content from "@/views/Salarys/SalaryComposition.vue";

// import toast thông báo
import { state } from "@/utils/toast.js";
import NotificationToast from "@/components/feedback/NotificationToast.vue";
</script>
