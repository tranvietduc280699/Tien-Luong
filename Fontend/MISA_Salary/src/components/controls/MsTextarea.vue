<template>
  <div class="input-text" tabindex="0">
    <textarea
      :value="modelValue"
      type="text"
      :placeholder="placeholder"
      @input="updateValue"
    ></textarea>
  </div>
</template>
<script setup>
defineProps({
  placeholder: {
    type: String,
    default: "",
  },
  modelValue: String,
});
const emit = defineEmits("update:modelValue");
const updateValue = (e) => {
  emit("update:modelValue", e.target.value);
};
</script>
<style scoped lang="scss">
.input-text {
  height: 71px;
  width: 676px;
  border-radius: 4px;
  border: 1px solid #e0e0e0;
  background: #fff;

  textarea {
    height: 100%;
    width: 100%;
    border: none;
    resize: none;
    padding: 12px;
  }
}
.input-text:focus {
  border: 1px solid #34b057;
}
.input-text:hover {
  border-color: #34b057;
}
</style>
