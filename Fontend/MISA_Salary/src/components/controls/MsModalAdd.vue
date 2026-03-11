<template>
  <div class="modal-overlay">
    <div class="coating"></div>
    <div class="modal-box">
      <div style="height: 53px">
        <div class="modal-header">
          <div class="title">Thông báo</div>
          <div class="modal-delete" @click="goBack">
            <div class="icon-close"></div>
          </div>
        </div>
      </div>

      <div class="modal-body">
        Thông tin bạn đã được thay đổi. Bạn có muốn lưu lại không ?
      </div>
      <div class="modal-footer">
        <div class="btn-edit" @click="closeModal">
          <div class="cancel">Hủy</div>
        </div>
        <div class="btn-edit" @click="goBack">
          <div class="cancel">Không lưu</div>
        </div>
        <div class="btn-delete" @click="saveValue">
          <div class="delete">Lưu</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { useRouter } from "vue-router";
///
///  điều hướng router
///
const router = useRouter();
const goBack = () => {
  router.push("/salarycomposition");
};
//nhận giá trị
// gửi lên cho cha
const emit = defineEmits(["closeModal", "saveValue"]);
const closeModal = () => {
  emit("closeModal");
};
const saveValue = () => {
  emit("saveValue");
};
</script>
<style scoped lang="scss">
.modal-overlay {
  transition: all 0.2s;
  width: 100%;
  height: 100%;
  position: fixed;
  top: 0;
  z-index: 100;
  display: flex;
  align-items: flex-start;
  padding-top: 70px;
  justify-content: center;
  opacity: 1;

  .coating {
    width: 100%;
    background: rgba(0, 0, 0, 0.4);
    position: absolute;
    top: 0;
    height: 100%;
    z-index: 10;
    transition: all 0.25s ease;
    opacity: 1;
  }

  /* Box */
  .modal-box {
    background: rgb(255, 255, 255);
    width: 415px;
    min-width: 500px;
    position: relative;
    z-index: 100;
    margin: 10px;
    max-width: calc(100% - 30px);
    border-radius: 6px;
    box-shadow: 0 5px 20px 0 rgba(0, 0, 0, 0.1);
    background: #fff;
    overflow: visible;

    .modal-body {
      padding: 24px;
      font-size: 14px;
      font-weight: 400px;
      color: #333;
      height: 69px;

      .text {
        color: #000;
        font-weight: 600;
        font-size: 14px;
      }
    }

    .modal-body-list {
      padding: 24px;
      font-size: 16px;
      color: #333;
    }

    /* Header */
    .modal-header {
      width: 100%;
      display: flex;
      align-items: center;
      justify-content: space-between;
      border-radius: 4px 4px;
      position: relative;
      padding: 24px;
      padding-bottom: 0;
      min-height: 53px !important;

      .title {
        letter-spacing: 0.576px;
        font-size: 24px;
        font-weight: 700px;
      }

      .modal-delete {
        height: 36px;
        width: 36px;
        display: flex;
        border-radius: 50%;
        justify-content: center;
        align-items: center;
      }

      .modal-delete:hover {
        background-color: #f0f4f2;
      }
    }

    .modal-footer {
      padding: 16px 24px;
      height: 68px;
      display: flex;
      justify-content: flex-end;
      background: #f2f2f2;
      padding: 16px 24px;
      border-radius: 0 0 4px 4px;

      .btn-edit {
        width: auto !important;
        height: 36px !important;
        border-radius: 4px;
        padding: 0px 16px;
        min-width: 80px !important;
        overflow: hidden;
        background-color: #fff;
        color: #212121;
        border: 1px solid #ebebeb;
        display: flex;
        justify-content: center;
        align-items: center;
        margin-right: 8px;
      }

      .btn-edit:hover {
        border-color: #2ca64a;
        color: #2ca64a;
        cursor: pointer;
      }

      .btn-delete {
        width: auto !important;
        height: 36px !important;
        border-radius: 4px;
        padding: 0px 16px;
        min-width: 80px !important;
        color: #fff;
        font-size: 14px;
        display: flex;
        justify-content: center;
        align-items: center;
        cursor: pointer;
        background-color: #34b057;
        border: 1px solid #34b057;
      }
    }
  }
}
</style>
