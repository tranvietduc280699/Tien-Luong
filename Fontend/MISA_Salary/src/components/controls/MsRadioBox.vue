<template>
  <div class="radio-box">
    <div
      class="radio-item"
      v-for="item in items"
      :key="item.value"
      @click="selectedItem(item)"
    >
      <div class="select-radio" :class="{ active: item.value == modelValue }">
        <div class="radio-circle"><div class="inRadio"></div></div>
      </div>
      <div class="text_label">
        <span>{{ item.label }}</span>
      </div>
    </div>
  </div>
</template>
<script setup>
const props = defineProps({
  items: {
    type: Array,
    default: () => [],
  },
  modelValue: {
    type: [String, Number],
    default: null,
  },
});
const emit = defineEmits(["update:modelValue"]);
const selectedItem = (item) => {
  emit("update:modelValue", item.value);
};
</script>
<style scoped lang="scss">
// lựa chọn trong tính chất
.radio-box {
  display: flex;
  gap: 32px;
}
.radio-item {
  display: flex;
  align-items: center;
  cursor: pointer;
  font-size: 14px;
  color: #212121;

  /* Vòng ngoài  */
  .select-radio {
    min-width: 30px;
    width: 30px;
    height: 30px;
    border-radius: 50%;
    display: flex;
    align-items: center;
    justify-content: center;
    /* Vòng trong – 20x20 */
    .radio-circle {
      min-width: 16px;
      width: 16px;
      height: 16px;
      border-radius: 50%;
      transition: 0.2s ease;
      border-radius: 50%;
      border: 3px solid #9e9e9e;
      display: flex;
      align-items: center;
      justify-content: center;
      transition: 0.2s ease;
    }
  }

  /* Hover toàn bộ vòng ngoài */
  .select-radio:hover {
    background: #e9e7e7;
  }
}
// sự kiện click
.select-radio.active .radio-circle {
  border: 3px solid #34b057;
  display: flex;
  align-items: center;
  justify-content: center;

  .inRadio {
    border-radius: 50%;
    min-width: 8px;
    width: 8px;
    height: 8px;
    background-color: #34b057;
    display: flex;
    align-items: center;
    justify-content: center;
  }
}
</style>
