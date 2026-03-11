<template>
  <div class="nabarx">
    <!-- header bên trái + tìm kiếm -->
    <div class="ms-navbar flex-center">
      <!-- phần đầu tiên header bên trái -->
      <div class="ms-navbar--title flex-center">
        <div class="box-app-menu flex-center">
          <div class="icon-ms-tooltip"></div>
        </div>
        <div class="logo flex-center">
          <div class="logo-img">
            <div class="icon-logo"></div>
          </div>
          <div class="logo-text">{{ $t("header.salary") }}</div>
        </div>
      </div>
      <!-- phần tìm kiếm trên header -->
      <div class="search-navbar flex-center">
        <div class="search-bar p-relative">
          <div class="ms-input flex">
            <div class="ms-icon-default flex-center" style="padding-left: 8px">
              <div class="icon-default"></div>
            </div>
            <input type="text" placeholder="Tìm kiếm" />
          </div>
        </div>
      </div>
    </div>
    <!-- icon bên phẩn phải header -->

    <div class="account-box flex-center">
      <div class="button_icon flex-center">
        <div class="icon_notification_version"></div>
      </div>
      <div class="button_icon flex-center">
        <div class="icon_cart"></div>
        <div class="badge">2</div>
      </div>
      <div class="button_icon flex-center">
        <div class="icon_mobileAds"></div>
        <div class="badge_mobileAds">New</div>
        <div class="star_mobileAds"></div>
      </div>
      <div class="button_icon flex-center">
        <div class="icon_invite"></div>
      </div>
      <div class="button_icon flex-center">
        <div class="icon-message"></div>
      </div>
      <div class="button_icon flex-center">
        <div class="icon-notification"></div>
        <div class="badge">62</div>
      </div>
      <div class="button_icon flex-center">
        <div class="icon-helpMenu"></div>
      </div>
      <div class="button_icon flex-center">
        <div class="icon-moreMenu"></div>
      </div>
      <div class="button_icon flex-center">
        <div class="avt">
          <!-- vòng ngoài vs số 1 -->
          <div class="misaid-update-border">
            <div class="misaid-missing-field">1</div>
            <div class="misaid-missing-border-percent"></div>
          </div>
          <!-- phần ảnh -->
          <div class="misaid-avatar-account">
            <div class="avt-MenuAccount"></div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup></script>

<style scoped lang="scss">
.p-relative {
  position: relative;
}

//style chung
.flex {
  display: flex;
}

.items-center {
  align-items: center !important;
}

//
//phần đầu header
//
.nabarx {
  display: flex;
  justify-content: space-between;
  height: 48px;
  width: 100%;
  box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.16);
  position: relative;
  // <!-- phần đầu tiên header bên trái -->
  .ms-navbar {
    background-image: var(--icon-cherryBlossom), var(--icon-background);
    background-repeat: no-repeat, no-repeat;
    .ms-navbar--title {
      display: flex;
      font-size: 1.1rem;
      padding: 0;
      padding-right: 24px;

      //icon đầu tiên trong header(menu)
      .box-app-menu {
        display: flex;
        align-items: center;
        justify-content: center;
        position: relative;
        margin: 0 16px;
        cursor: pointer;
        width: 24px;
        height: 24px;
        border-radius: 50%;
      }
      .box-app-menu:hover {
        background-color: #f7e8ed;
      }
      .logo-img {
        width: 32px;
        height: 32px;
        overflow: hidden;
        cursor: pointer;
      }
      .logo-text {
        margin-left: 12px;
        font-size: 18px;
        color: #000;
        font-weight: 700;
      }
    }

    // thanh tìm kiếm trên header
    .search-navbar {
      height: 48px;
      flex: 1;

      .search-bar {
        display: flex;
        width: fit-content;
        height: 32px;
        width: 100%;

        .ms-input {
          width: 300px;
          border-radius: 4px;
          background: #f6f6f6;
          height: 32px;
          align-items: center;
          overflow: hidden;
          .ms-icon-default {
            border: none;
            background: #f6f6f6;
            width: 24px;
            height: 24px;
          }

          input {
            height: 32px;
            border: none;
            width: 100%;
            background: #f6f6f6;
            padding: 0 12px;
            outline: none;
          }
        }
      }
    }
  }
  //<!-- icon bên phẩn phải header -->
  .account-box {
    margin-right: 12px;

    .button_icon {
      margin-right: 12px;
      position: relative;
      cursor: pointer;
      width: 32px;
      height: 32px;
      display: flex;
      align-items: center;
      justify-content: center;
      border-radius: 50%;
    }

    //hover icon trên header
    .button_icon:hover {
      background-color: #f2f2f2;
    }

    .avt {
      margin-left: 4px;
      position: relative;
      min-width: 32px;
      min-height: 32px;

      .misaid-update-border {
        display: block;
        cursor: pointer;
        position: relative;

        .misaid-missing-field {
          position: absolute;
          top: -3px;
          right: -3px;
          width: 16px;
          height: 16px;

          background: #f5222d;
          border-radius: 50%;
          color: #fff;
          font-size: 10px;
          line-height: 16px;
          font-weight: 700;
          text-align: center;
          z-index: 10;
        }

        .misaid-missing-border-percent {
          padding: 3px;
          width: 32px;
          height: 32px;

          background: conic-gradient(
            rgb(65, 146, 255) 80%,
            rgb(245, 34, 45) 80%
          );
          border-radius: 50%;
          box-sizing: border-box;
        }
      }

      .misaid-avatar-account {
        position: absolute;
        top: 50%;
        left: 50%;
        width: 24px;
        height: 24px;
        transform: translate(-50%, -50%);
        border-radius: 50%;
        overflow: hidden;
      }
    }

    // tạo thông báo tạm thời trên icon header
    .badge {
      /* Chồng lên icon */
      position: absolute;
      top: -4px;
      right: -3px;
      background: #ff2929;
      color: #fff;
      border-radius: 50%;
      font-size: 11px;
      padding: 2px 6px;
      font-weight: bold;
      line-height: 1;
    }

    // tạo new vs img trên mobile phần header
    .badge_mobileAds {
      background-color: #ff2929;
      color: #fff;
      position: absolute;
      top: -4px;
      right: -8px;
      padding: 0 3px;
      border-radius: 24px;
      height: 16px;
      line-height: 16px;
      font-size: 10px;
      user-select: none;
      font-weight: 700;
    }

    //tạo ngồi sao trên icon mibile
    .star_mobileAds {
      position: absolute;
      top: -12px;
      right: 12px;
    }
  }
}
</style>
