import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
import path from "path";

export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
    },
  },
  define: {
    "DevExpress.License": '""',
  },
  server: {
    port: 9898,
   hmr: {
      protocol: 'ws',
      host: 'localhost',
    }
  }
});
``