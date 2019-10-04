mergeInto(LibraryManager.library, {

  OpenPageInNewTab: function (url) {
    url = Pointer_stringify(url);
    console.log('Opening link: ' + url);
    window.open(url,'_blank');
  },

  BindWebGLTexture: function (texture) {
    GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[texture]);
  },

});