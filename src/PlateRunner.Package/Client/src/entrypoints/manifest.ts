export const manifests: Array<UmbExtensionManifest> = [
  {
    name: "Plate Runner Package Entrypoint",
    alias: "PlateRunner.Package.Entrypoint",
    type: "backofficeEntryPoint",
    js: () => import("./entrypoint.js"),
  },
];
