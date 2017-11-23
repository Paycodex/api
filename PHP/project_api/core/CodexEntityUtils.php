<?php

/**\brief Helper estático para tratar con instancias HttpEntity.
 *
 * 
 * 
 * @author koiosoft
 *
 */
class CodexEntityUtils
{
	
	/**
	 * 
	 * @param CodexHttpEntity $entity
	 */
	public static function toString($entity)
	{
		return $entity->getContent();
	}
	
}