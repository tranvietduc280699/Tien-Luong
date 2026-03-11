<template>
  <div id="app">
    <!-- <MainLayout /> -->
    <RouterView />
  </div>
</template>
<script setup>
//import MainLayout from "@/layouts/MainLayout.vue";
</script>
<style lang="scss">
html {
  font-size: 14px;
  width: 100%;
  line-height: 1.5;
  letter-spacing: .01rem;
}

body,
#app {
  margin: 0;
  padding: 0;
  height: 100%;
  overflow: hidden;
  font-size: 14px;
}

.body-layout {
  display: flex;
}

* {
  box-sizing: border-box;
  outline: none;
  margin: 0;
  padding: 0;
}
</style>
