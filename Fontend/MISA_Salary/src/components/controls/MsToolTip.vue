<template>
  <DxTooltip
    :target="target"
    :visible="visible"
    @hiding="onHiding"
    show-event="none"
    hide-event="none"
    position="bottom"
  >
    <template #content>
      <div class="ms-tooltip-content">{{ content }}</div>
    </template>
  </DxTooltip>
</template>

<script setup>
import { DxTooltip } from "devextreme-vue/tooltip";

const props = defineProps({
  target: {},
  visible: Boolean,
  content: String,
});

const emit = defineEmits(["update:visible"]);

const onHiding = () => {
  emit("update:visible", false);
};
</script>

<style scoped lang="scss"></style>
