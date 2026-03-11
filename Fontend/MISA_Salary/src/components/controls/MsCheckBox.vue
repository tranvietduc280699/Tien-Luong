<template>
  <div class="tax-option">
    <div class="checkbox-wrap" @click="selectCheckBox()">
      <div class="checkbox-box" :class="{ active: modelValue }">
        <div class="iconSelected" v-if="modelValue"></div>
      </div>
    </div>
    <div class="checkbox-label">
      {{ label }}
    </div>
  </div>
</template>

<script setup>
import { ref } from "vue";
// const activeCheckBox = ref(false);
// const selectCheckBox =(modelValue) =>{
//     activeCheckBox.value = !activeCheckBox.value;
// }
const emit = defineEmits(["update:modelValue"]);
const selectCheckBox = () => {
  emit("update:modelValue", !props.modelValue);
};
const props = defineProps({
  label: String,
  modelValue: Boolean,
});
</script>

<style scoped lang="scss">
.tax-option {
  display: flex; // 2 cái theo chiều ngang
  align-items: center;
  margin-left: 16px;
  cursor: pointer;
  /* div bọc để hover hình tròn */
  .checkbox-wrap {
    width: 30px;
    height: 30px;
    border-radius: 50%;
    display: flex;
    align-items: center;
    justify-content: center;
    transition: background 0.2s ease;

    &:hover {
      background: #f3f3f3;
    }
    /* ô vuông bên trong */
    .checkbox-box {
      width: 16px;
      height: 16px;
      border: 3px solid #9e9e9e;
      border-radius: 4px;
    }

    /* chữ bên phải */
    .checkbox-label {
      margin-left: 8px;
      font-size: 14px;
      color: #333;
      user-select: none;
    }
    // trạng thái active
    .active {
      background-color: #34b057;
      border: 1px solid #34b057;
      border-radius: 4px;
      display: flex;
      align-items: center;
      justify-content: center;
    }
  }
}
</style>
