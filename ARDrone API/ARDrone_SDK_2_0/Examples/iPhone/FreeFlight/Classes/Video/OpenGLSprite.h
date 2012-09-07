/**
 *  @file OpenGLSprite.h
 *
 * Copyright 2009 Parrot SA. All rights reserved.
 * @author D HAEYER Frederic
 * @date 2009/10/26
 */

#import "ARDroneTypes.h"
#import "ARDrone.h"
#include "opengl_texture.h"


@interface OpenGLSprite : NSObject {
	ARDroneSize	 screen_size;
	ARDroneSize    old_size;

@protected
    BOOL screenOrientationRight;
    BOOL screenOrientationChanged;
	void            *default_image;
	ARDroneScaling	 scaling;
	ARDroneOpenGLTexture *texture;
    GLuint           program;
}

@property (nonatomic, assign) BOOL screenOrientationRight;

- (id)initWithFrame:(CGRect)frame withScaling:(ARDroneScaling)_scaling withProgram:(GLuint)programId withDrone:(ARDrone*)drone;
- (void)drawSelf;
- (void)setScaling:(ARDroneScaling)newScaling;
- (ARDroneScaling)getScaling;

@end
