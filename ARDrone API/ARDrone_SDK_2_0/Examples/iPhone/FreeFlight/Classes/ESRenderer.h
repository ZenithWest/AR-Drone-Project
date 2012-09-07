//
//  ESRenderer.h
//  FreeFlight
//
//  Created by Frédéric D'HAEYER on 24/10/11.
//  Copyright Parrot SA 2009. All rights reserved.
//
#import <QuartzCore/QuartzCore.h>
#import <OpenGLES/ES2/gl.h>
#import <OpenGLES/ES2/glext.h>
#import "ARDroneProtocols.h"
#import "ARDrone.h"
#import "OpenGLSprite.h"
#import "opengl_shader.h"

@interface ESRenderer : NSObject
{
@private
	EAGLContext *context;

	// The OpenGL names for the framebuffer and renderbuffer used to render to this view
	GLuint defaultFramebuffer, colorRenderbuffer;

	GLuint textureId[2];
	GLuint vertexBufferId;
	GLuint indexBufferId;
    GLuint programId;
    OpenGLSprite* video;
}
- (id) initWithFrame:(CGRect)frame andDrone:(ARDrone*)drone;
- (void) render:(ARDrone *)instance;
- (void)setScreenOrientationRight:(BOOL)right;
- (BOOL) resizeFromLayer:(CAEAGLLayer *)layer;
@end

