/**
 *  @file OpenGLSprite.m
 *
 * Copyright 2009 Parrot SA. All rights reserved.
 * @author D HAEYER Frederic
 * @date 2009/10/26
 */
#import "OpenGLSprite.h"

static CGFloat const matrixOrthoFront[] = {1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, -1.0f, 1.0f};
static CGFloat const matrixOrthoBack[] = {1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f};

static CGFloat const matrixOrthoBackLeft[] = {1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f};
static CGFloat const matrixOrthoBackRight[] = {-1.0f, 0.0f, 0.0f, 0.0f, 0.0f, -1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f};

static CGFloat const identityMatrix[] = {1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f};

static GLfloat mvp[16] = { 0 };

@implementation OpenGLSprite
@synthesize screenOrientationRight;

- (id)initWithFrame:(CGRect)frame withScaling:(ARDroneScaling)_scaling withProgram:(GLuint)programId withDrone:(ARDrone*)drone
{
	if ((self = [super init]))
	{
		// ScreenSize
		screen_size.width = frame.size.width;
		screen_size.height = frame.size.height;
		scaling = _scaling;
        program = programId;
        texture = [drone videoTexture];
        opengl_texture_init(texture);
		
        texture->bytesPerPixel = 2;
		texture->format = GL_RGB;
		texture->type = GL_UNSIGNED_SHORT_5_6_5;
		texture->image_size.width = 1;
		texture->image_size.height = 1;
		texture->texture_size.width = 1;
		texture->texture_size.height = 1;
		
		opengl_texture_scale_compute(texture, screen_size, scaling);
        memcpy(&old_size, &texture->image_size, sizeof(ARDroneSize));

        screenOrientationChanged = NO;
        self.screenOrientationRight = NO;

		// Allocate memory to store the texture data
		default_image = malloc(texture->texture_size.width * texture->texture_size.height * texture->bytesPerPixel);
        memset(default_image, 0, texture->texture_size.width * texture->texture_size.height * texture->bytesPerPixel);
		texture->data = default_image;
    }

	return self;
}

- (void)setScreenOrientationRight:(BOOL)_screenOrientationRight
{
    screenOrientationRight = _screenOrientationRight;
    memcpy(mvp, screenOrientationRight ? matrixOrthoBackRight : matrixOrthoBackLeft, sizeof(GLfloat) * 16);
    mvp[0] *= texture->scaleModelX;
    mvp[5] *= texture->scaleModelY;
    screenOrientationChanged = YES;
}

/*- (void)setScreenOrientationRight:(BOOL)_screenOrientationRight
{
    screenOrientationRight = _screenOrientationRight;

    memcpy(mvp,matrixOrthoBackRight , sizeof(GLfloat) * 16);
    mvp[0] *= texture->scaleModelX;
    mvp[5] *= texture->scaleModelY;
    screenOrientationChanged = YES;
}*/

- (BOOL)checkNewResolution
{
	BOOL result = NO;
	if ((old_size.width != texture->image_size.width) || (old_size.height != texture->image_size.height)) 
	{
		NSLog(@"%s old_size : %d, %d - texture : %d, %d", __FUNCTION__, (int)old_size.width, (int)old_size.height, (int)texture->image_size.width, (int)texture->image_size.height);
		opengl_texture_scale_compute(texture, screen_size, scaling);
        [self setScreenOrientationRight:screenOrientationRight];
        memcpy(&old_size, &texture->image_size, sizeof(ARDroneSize));
		result = YES;
	}
	
	return result;
}

- (void)drawSelf
{
   // opengl_context_t context;
    BOOL new_resolution = [self checkNewResolution];
        
    if(screenOrientationChanged)
    {
        // Get uniform locations.
        GLuint mvpUniform = glGetUniformLocation(program, "mvp");
        // set the mvp uniform
        glUniformMatrix4fv(mvpUniform, 1, GL_FALSE, mvp);
        screenOrientationChanged = NO;
    }
        
    // Bind the background texture
    // Draw video
    if(new_resolution)
    {            
        GLuint texScaleUniform = glGetUniformLocation(program, "texscale");
        GLfloat textureScaleMatrix[] =
        {
            texture->scaleTextureX, 0.0f,
            0.0f, texture->scaleTextureY,
        };
        glUniformMatrix2fv(texScaleUniform, 1, GL_FALSE, textureScaleMatrix);
    }
        
    opengl_texture_draw(texture, program);
    
    // Validate program before drawing. This is a good check, but only really necessary in a debug build.
    // DEBUG macro must be defined in your debug configurations if that's not already the case.
#ifdef DEBUG_SHADER
    if (!opengl_shader_validate(program))
    {
        NSLog(@"Failed to validate program: %d", program);
        return;
    }
#endif
}

- (void)setScaling:(ARDroneScaling)newScaling
{
    scaling = newScaling;
    opengl_texture_scale_compute(texture, screen_size, newScaling);
}

- (ARDroneScaling)getScaling
{
    return scaling;
}

- (void)dealloc
{
	// destroy the texture
	opengl_texture_destroy(texture);
    
	// Free the memory allocated to store the texture data
	free(default_image);
    free(texture);
    
	[super dealloc];
}
@end
