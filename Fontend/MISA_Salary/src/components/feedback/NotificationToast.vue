<template>
  <div class="toast-wrapper">
    <!-- thành công -->
    <div class="toast-success" v-if="props.toastSuccess !== ''">
      <div class="icon-success">
        <div class="icon_toastSuccess"></div>
      </div>

      <div class="toast-text-success">{{ props.toastSuccess }}</div>
    </div>
    <!--  thất bại -->
    <div class="toast-error" v-if="props.toastError !== ''">
      <div class="icon-error">
        <div class="icon_toastError"></div>
      </div>
      <div class="toast-text-error">{{ props.toastError }}</div>
    </div>
  </div>
</template>

<script setup>
import { watch } from "vue";

// nhận từ MainLayout
const props = defineProps({
  toastSuccess: String,
  toastError: String,
  toastWarning: String,
});
watch(
  [() => props.toastSuccess, () => props.toastError, () => props.toastWarning],
  (newS, newE, neW) => {
    console.log("giá trị nhận để hiển thị là:", [newS, newE, neW]);
  },
  { immediate: true },
);
</script>
<style scoped lang="scss">
.toast-wrapper {
  position: absolute;
  display: flex;
  justify-items: center;
  top: 75px;
  left: 50%;
  transform: translateX(-50%);
  z-index: 9999;
  // thành công
  .toast-success {
    display: flex;
    align-items: stretch;
    height: 36px;
    background-color: #bdf3d8;
    border: 1px solid #00ca4e;
    border-radius: 4px;
    overflow: hidden;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.05);
    .icon-success {
      background-color: #00ca4e;
      width: 36px;
      flex-shrink: 0;
      display: flex;
      justify-content: center;
      align-items: center;
    }

    /* Phần text (bên phải) */
    .toast-text-success {
      display: flex;
      align-items: center;
      color: #262626;
      font-size: 14px;
      font-weight: 400;
      white-space: nowrap;
      padding: 0px 12px;
    }
  }
  
  // thất bại
  .toast-error {
    display: flex;
    align-items: stretch;
    height: 36px;
    background-color: #ffeaea;
    border: 1px solid red;
    border-radius: 4px;
    overflow: hidden;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.05);
    .icon-error {
      background-color: red;
      width: 36px;
      flex-shrink: 0;
      display: flex;
      justify-content: center;
      align-items: center;
    }

    /* Phần text (bên phải) */
    .toast-text-error {
      display: flex;
      align-items: center;
      color: #262626;
      font-size: 14px;
      font-weight: 400;
      white-space: nowrap;
      padding: 0px 12px;
    }
  }
}
</style>
